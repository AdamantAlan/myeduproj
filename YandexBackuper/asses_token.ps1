$ErrorActionPreference = "Stop"

$clientId = ""
$clientSecret = ""
$code = ""

try {
    $response = Invoke-RestMethod `
        -Uri "https://oauth.yandex.ru/token" `
        -Method Post `
        -ContentType "application/x-www-form-urlencoded" `
        -Body @{
            grant_type = "authorization_code"
            code = $code
            client_id = $clientId
            client_secret = $clientSecret
        }

    if ([string]::IsNullOrWhiteSpace($response.access_token)) {
        throw "Access Token отсутствует в ответе"
    }

    $env:YANDEX_DISK_TOKEN = $response.access_token

    Write-Host "Access Token успешно получен!" -ForegroundColor Green
}
catch {
    Write-Host "Ошибка получения токена: $($_.Exception.Message)" -ForegroundColor Red
}