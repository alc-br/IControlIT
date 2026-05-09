<#
.SYNOPSIS
    Hardening Schannel/IIS para fechar a vulnerabilidade
    "Insecure Transport: Weak SSL Protocols" do relatorio VAPT/SOC.

.DESCRIPTION
    Idempotente. Pode ser executado quantas vezes quiser - so altera o
    que ainda nao esta no estado desejado.

    O script faz:
      1. Backup do registro Schannel atual em C:\IC1\Backups\Schannel\
      2. Desabilita SSL 2.0, SSL 3.0, TLS 1.0, TLS 1.1 (Client e Server)
      3. Habilita TLS 1.2 (Client e Server)
      4. Desliga cifras fracas: RC4 (todas), DES 56, 3DES 168, NULL
      5. Desliga hash MD5
      6. Define ordem moderna de cipher suites (ECDHE + AES-GCM no topo)
      7. Loga tudo em C:\IC1\Logs\Harden-Schannel_<timestamp>.log
      8. NAO reinicia sozinho - exige reboot manual no final

.PARAMETER WhatIf
    Modo dry-run. Mostra o que seria alterado sem alterar nada.

.PARAMETER SkipBackup
    Pula o backup do registro. NAO RECOMENDADO. Use so se ja tiver
    backup feito por outro meio (snapshot da VM, por exemplo).

.EXAMPLE
    # Dry-run: ver o que seria feito sem alterar nada
    .\Harden-Schannel.ps1 -WhatIf

.EXAMPLE
    # Execucao real (precisa de PowerShell como Administrador)
    .\Harden-Schannel.ps1

.NOTES
    Autor:  Anderson Luiz Chipak - Agencia ALC
    Data:   09/05/2026
    Origem: feature-vapt-soc-080520260830 (relatorio Scrut/Riversys 29/01/2026)

    PRE-REQUISITO: PowerShell rodando como Administrador.
    POS-REQUISITO: REBOOT do servidor para o Schannel aplicar as mudancas.

    Validacao apos reboot:
      - testssl.sh <dominio>:443
      - https://www.ssllabs.com/ssltest/ (Qualys SSL Labs) - meta nota A
#>

[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [switch]$SkipBackup
)

# ----------------------------------------------------------------------------
# Setup: log + verificacao de admin
# ----------------------------------------------------------------------------
$ErrorActionPreference = 'Stop'

$timestamp = Get-Date -Format 'yyyyMMdd_HHmmss'
$logDir    = 'C:\IC1\Logs'
$backupDir = 'C:\IC1\Backups\Schannel'
$logFile   = Join-Path $logDir "Harden-Schannel_$timestamp.log"

if (-not (Test-Path $logDir))    { New-Item -ItemType Directory -Path $logDir    -Force | Out-Null }
if (-not (Test-Path $backupDir)) { New-Item -ItemType Directory -Path $backupDir -Force | Out-Null }

function Write-Log {
    param([string]$Message, [string]$Level = 'INFO')
    $line = "[{0}] [{1}] {2}" -f (Get-Date -Format 'yyyy-MM-dd HH:mm:ss'), $Level, $Message
    Add-Content -Path $logFile -Value $line
    switch ($Level) {
        'ERROR'   { Write-Host $line -ForegroundColor Red }
        'WARN'    { Write-Host $line -ForegroundColor Yellow }
        'CHANGE'  { Write-Host $line -ForegroundColor Cyan }
        'OK'      { Write-Host $line -ForegroundColor Green }
        default   { Write-Host $line }
    }
}

# Verifica admin
$isAdmin = ([Security.Principal.WindowsPrincipal] `
            [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole(
            [Security.Principal.WindowsBuiltInRole]::Administrator)

if (-not $isAdmin) {
    Write-Log "Este script precisa ser executado como Administrador. Abortando." 'ERROR'
    throw "Execute o PowerShell como Administrador e rode novamente."
}

Write-Log "=========================================================="
Write-Log "Harden-Schannel iniciado em $env:COMPUTERNAME"
Write-Log "Modo: $(if ($WhatIfPreference) { 'WhatIf (dry-run)' } else { 'EXECUCAO REAL' })"
Write-Log "Log:    $logFile"
Write-Log "Backup: $backupDir"
Write-Log "=========================================================="

# ----------------------------------------------------------------------------
# 1. Backup do registro
# ----------------------------------------------------------------------------
if (-not $SkipBackup) {
    $backupFile = Join-Path $backupDir "Schannel_$timestamp.reg"
    Write-Log "Fazendo backup do registro Schannel em $backupFile" 'CHANGE'
    if ($PSCmdlet.ShouldProcess("HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL", "Exportar para $backupFile")) {
        & reg.exe export "HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL" "$backupFile" /y | Out-Null
        if ($LASTEXITCODE -eq 0) {
            Write-Log "Backup concluido." 'OK'
        } else {
            Write-Log "Falha no backup (exit $LASTEXITCODE). Abortando por seguranca." 'ERROR'
            throw "Backup do registro falhou."
        }
    }
} else {
    Write-Log "Backup pulado por solicitacao (-SkipBackup)." 'WARN'
}

# ----------------------------------------------------------------------------
# Helpers
# ----------------------------------------------------------------------------
function Set-RegValue {
    param(
        [string]$Path,
        [string]$Name,
        [int]   $Value,
        [string]$Type = 'DWord',
        [string]$Description
    )

    if (-not (Test-Path $Path)) {
        if ($PSCmdlet.ShouldProcess($Path, "Criar chave")) {
            New-Item -Path $Path -Force | Out-Null
            Write-Log "Criada chave: $Path" 'CHANGE'
        }
    }

    $current = $null
    try { $current = (Get-ItemProperty -Path $Path -Name $Name -ErrorAction Stop).$Name } catch { }

    if ($current -eq $Value) {
        Write-Log "  [OK]     $Description (ja em $Value)" 'OK'
        return
    }

    $action = if ($null -eq $current) { "criar com valor $Value" } else { "alterar de $current para $Value" }
    if ($PSCmdlet.ShouldProcess("$Path\$Name", $action)) {
        New-ItemProperty -Path $Path -Name $Name -Value $Value -PropertyType $Type -Force | Out-Null
        Write-Log "  [CHANGE] $Description -> $action" 'CHANGE'
    }
}

# ----------------------------------------------------------------------------
# 2. Protocolos: desabilitar SSL2/3, TLS 1.0/1.1; habilitar TLS 1.2
# ----------------------------------------------------------------------------
Write-Log ""
Write-Log "[2] Protocolos SSL/TLS"

$protocolsBase = 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols'

$protocolosDesabilitar = @('SSL 2.0', 'SSL 3.0', 'TLS 1.0', 'TLS 1.1')
foreach ($proto in $protocolosDesabilitar) {
    foreach ($side in 'Client','Server') {
        $key = "$protocolsBase\$proto\$side"
        Set-RegValue -Path $key -Name 'Enabled'           -Value 0 -Description "$proto $side - Enabled=0"
        Set-RegValue -Path $key -Name 'DisabledByDefault' -Value 1 -Description "$proto $side - DisabledByDefault=1"
    }
}

# TLS 1.2 - garantir habilitado
foreach ($side in 'Client','Server') {
    $key = "$protocolsBase\TLS 1.2\$side"
    Set-RegValue -Path $key -Name 'Enabled'           -Value 1 -Description "TLS 1.2 $side - Enabled=1"
    Set-RegValue -Path $key -Name 'DisabledByDefault' -Value 0 -Description "TLS 1.2 $side - DisabledByDefault=0"
}

# ----------------------------------------------------------------------------
# 3. Cifras: desabilitar RC4, DES 56, 3DES 168, NULL
# ----------------------------------------------------------------------------
Write-Log ""
Write-Log "[3] Cifras fracas"

$ciphersBase = 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers'

$cifrasDesabilitar = @(
    'RC4 40/128',
    'RC4 56/128',
    'RC4 64/128',
    'RC4 128/128',
    'DES 56/56',
    'Triple DES 168',
    'NULL'
)

foreach ($cipher in $cifrasDesabilitar) {
    # Nome da chave precisa de barra invertida escapada no path do registro
    $key = "$ciphersBase\$cipher"
    Set-RegValue -Path $key -Name 'Enabled' -Value 0 -Description "Cipher $cipher - Enabled=0"
}

# Garantir AES habilitado
foreach ($cipher in 'AES 128/128','AES 256/256') {
    $key = "$ciphersBase\$cipher"
    Set-RegValue -Path $key -Name 'Enabled' -Value 0xFFFFFFFF -Description "Cipher $cipher - Enabled=ON"
}

# ----------------------------------------------------------------------------
# 4. Hashes: desabilitar MD5
# ----------------------------------------------------------------------------
Write-Log ""
Write-Log "[4] Hashes fracos"

$hashesBase = 'HKLM:\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Hashes'
Set-RegValue -Path "$hashesBase\MD5" -Name 'Enabled' -Value 0 -Description "Hash MD5 - Enabled=0"

# ----------------------------------------------------------------------------
# 5. Ordem das cipher suites - prioriza ECDHE + AES-GCM
# ----------------------------------------------------------------------------
Write-Log ""
Write-Log "[5] Ordem das cipher suites"

$cipherOrder = @(
    'TLS_ECDHE_ECDSA_WITH_AES_256_GCM_SHA384',
    'TLS_ECDHE_ECDSA_WITH_AES_128_GCM_SHA256',
    'TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384',
    'TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256',
    'TLS_DHE_RSA_WITH_AES_256_GCM_SHA384',
    'TLS_DHE_RSA_WITH_AES_128_GCM_SHA256',
    'TLS_ECDHE_ECDSA_WITH_AES_256_CBC_SHA384',
    'TLS_ECDHE_ECDSA_WITH_AES_128_CBC_SHA256',
    'TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA384',
    'TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA256'
) -join ','

$cipherOrderKey = 'HKLM:\SOFTWARE\Policies\Microsoft\Cryptography\Configuration\SSL\00010002'

if (-not (Test-Path $cipherOrderKey)) {
    if ($PSCmdlet.ShouldProcess($cipherOrderKey, "Criar chave de ordem de cipher suites")) {
        New-Item -Path $cipherOrderKey -Force | Out-Null
        Write-Log "Criada chave: $cipherOrderKey" 'CHANGE'
    }
}

$currentOrder = $null
try { $currentOrder = (Get-ItemProperty -Path $cipherOrderKey -Name 'Functions' -ErrorAction Stop).Functions } catch { }

if ($currentOrder -eq $cipherOrder) {
    Write-Log "  [OK]     Ordem de cipher suites ja configurada." 'OK'
} else {
    if ($PSCmdlet.ShouldProcess("$cipherOrderKey\Functions", "Definir ordem de cipher suites")) {
        New-ItemProperty -Path $cipherOrderKey -Name 'Functions' -Value $cipherOrder -PropertyType String -Force | Out-Null
        Write-Log "  [CHANGE] Ordem de cipher suites atualizada." 'CHANGE'
    }
}

# ----------------------------------------------------------------------------
# 6. .NET - SchUseStrongCrypto (forca .NET >= 4.5 a usar TLS forte)
# ----------------------------------------------------------------------------
Write-Log ""
Write-Log "[6] .NET SchUseStrongCrypto"

$netKeys = @(
    'HKLM:\SOFTWARE\Microsoft\.NETFramework\v4.0.30319',
    'HKLM:\SOFTWARE\WOW6432Node\Microsoft\.NETFramework\v4.0.30319'
)

foreach ($k in $netKeys) {
    Set-RegValue -Path $k -Name 'SchUseStrongCrypto'       -Value 1 -Description "$k\SchUseStrongCrypto"
    Set-RegValue -Path $k -Name 'SystemDefaultTlsVersions' -Value 1 -Description "$k\SystemDefaultTlsVersions"
}

# ----------------------------------------------------------------------------
# Final
# ----------------------------------------------------------------------------
Write-Log ""
Write-Log "=========================================================="
if ($WhatIfPreference) {
    Write-Log "DRY-RUN concluido. Nenhuma alteracao foi aplicada." 'WARN'
    Write-Log "Para aplicar de fato, execute novamente sem -WhatIf." 'WARN'
} else {
    Write-Log "Hardening concluido com sucesso." 'OK'
    Write-Log ""
    Write-Log "PROXIMO PASSO OBRIGATORIO: REBOOT DO SERVIDOR" 'WARN'
    Write-Log "  As mudancas no Schannel so entram em vigor apos reiniciar." 'WARN'
    Write-Log ""
    Write-Log "VALIDACAO POS-REBOOT:" 'WARN'
    Write-Log "  - testssl.sh <dominio>:443" 'WARN'
    Write-Log "  - https://www.ssllabs.com/ssltest/ (meta: nota A)" 'WARN'
}
Write-Log "Log completo: $logFile"
Write-Log "=========================================================="
