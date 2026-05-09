# infra/

Scripts de infraestrutura usados pelo IC1.

## Harden-Schannel.ps1

Resolve a vulnerabilidade **"Insecure Transport: Weak SSL Protocols"** apontada no relatorio VAPT (Scrut/Riversys, 29/01/2026).

### O que faz

1. Backup do registro atual em `C:\IC1\Backups\Schannel\`
2. Desabilita SSL 2.0, SSL 3.0, TLS 1.0, TLS 1.1
3. Habilita TLS 1.2
4. Desliga cifras fracas (RC4, DES, 3DES, NULL) e hash MD5
5. Define ordem moderna de cipher suites (ECDHE + AES-GCM)
6. Liga `SchUseStrongCrypto` no .NET 4.x (32 e 64 bits)
7. Loga tudo em `C:\IC1\Logs\Harden-Schannel_<timestamp>.log`

### Como usar

**1. Abrir PowerShell como Administrador.**

**2. Dry-run (recomendado primeiro):**
```powershell
cd D:\IC1\FONTE\infra
.\Harden-Schannel.ps1 -WhatIf
```
Mostra tudo que seria feito, sem alterar nada.

**3. Execucao real:**
```powershell
.\Harden-Schannel.ps1
```

**4. Reboot do servidor.** Schannel so aplica apos reiniciar.

**5. Validacao pos-reboot:**
- `testssl.sh <dominio>:443`
- https://www.ssllabs.com/ssltest/ (meta: nota A)
- Smoke test funcional do IC1

### Caracteristicas

- **Idempotente** — pode rodar varias vezes; so altera o que ainda precisa.
- **Logado** — cada execucao gera log com timestamp.
- **Reversivel** — backup `.reg` automatico antes de qualquer mudanca. Para desfazer: `reg.exe import C:\IC1\Backups\Schannel\Schannel_<timestamp>.reg` + reboot.
- **Sem reboot automatico** — voce escolhe quando reiniciar.

### Onde aplicar

1. **DEV primeiro** — rodar, reiniciar, validar com testssl, smoke test.
2. **PRD em janela de manutencao** — snapshot da VM antes, rodar, reiniciar, validar.
3. **Garantir igualdade DEV/PRD** apos os dois rodarem (combinado com K2A).

### Riscos

- Clientes externos do `WSChamado.asmx` ainda em TLS 1.0/1.1 perdem conexao. Inventariar antes.
- Navegadores muito antigos (IE10 sem patch, Android < 5) param de funcionar — hoje e irrelevante.
