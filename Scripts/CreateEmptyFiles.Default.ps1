# PowerShell script to create empty files with specified filenames in the current directory

$filenames = @"
stds9.d01e01.mkv
stds9.d01e02.mkv
stds9.d01e03.mkv
stds9.d01e04.mkv
stds9.d02e01.mkv
stds9.d02e02.mkv
stds9.d02e03.mkv
stds9.d02e04.mkv
stds9.d03e01.mkv
stds9.d03e02.mkv
stds9.d03e03.mkv
stds9.d03e04.mkv
stds9.d04e01.mkv
stds9.d04e02.mkv
stds9.d04e03.mkv
stds9.d04e04.mkv
stds9.d05e01.mkv
stds9.d05e02.mkv
stds9.d05e03.mkv
stds9.d05e04.mkv
stds9.d06e01.mkv
stds9.d06e02.mkv
stds9.d06e03.mkv
stds9.d06e04.mkv
stds9.d07e01.mkv
"@ -split "`n"

$subfolder = "TestFiles"
New-Item -ItemType Directory -Path $subfolder -Force | Out-Null

Get-ChildItem $subFolder -File | Remove-Item | Out-Null

foreach ($filename in $filenames) {
    $trimmed = $filename.Trim()
    if ($trimmed) {
        $filepath = Join-Path $subfolder $trimmed
        New-Item -ItemType File -Path $filepath -Force | Out-Null
    }
}