$ErrorActionPreference = 'Stop'

$endpoint = 'http://localhost:8083/sync/successfactors.asmx/SincronizarTodasAsAPIs'

$logDir = 'C:\Logs\SincronizarTodasAsAPIs'
if (-not (Test-Path $logDir)) { New-Item -ItemType Directory -Path $logDir -Force | Out-Null }
$logFile = Join-Path $logDir ("sync_{0:yyyyMMdd}.log" -f (Get-Date))

function Write-Log([string]$msg) {
    $line = "[{0:yyyy-MM-dd HH:mm:ss}] {1}" -f (Get-Date), $msg
    $line | Out-File -FilePath $logFile -Append -Encoding utf8
}

try {
    Write-Log "Iniciando SincronizarTodasAsAPIs"
    $resp = Invoke-WebRequest -Uri $endpoint -Method POST -UseBasicParsing -TimeoutSec 3600
    Write-Log ("OK StatusCode={0}" -f $resp.StatusCode)
} catch {
    Write-Log ("ERRO: " + $_.Exception.Message)
    exit 1
}
