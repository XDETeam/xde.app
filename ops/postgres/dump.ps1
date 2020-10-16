Clear-Host

Set-Location $PSScriptRoot

$settings = @{}
Get-Content -Path ../.env | ForEach-Object {
    $item = $_ -split '='
    $settings[$item[0]] = $item[1]
}

if ($settings.POSTGRES_PASSWORD) {
    $Env:PGPASSWORD = $settings.POSTGRES_PASSWORD
}

Start-Process `
    -FilePath "$($env:ProgramFiles)\PostgreSQL\13\bin\pg_dump.exe" `
    -Args @("-h", "localhost", "-p", "8132", "-d", "xde", "-U", "postgres") `
    -RedirectStandardOutput "c:\Users\stani\OneDrive\Ego\xde-$([datetime]::Now.ToString("yyyyMMdd-hhmmssfff")).sql" `
    -NoNewWindow
