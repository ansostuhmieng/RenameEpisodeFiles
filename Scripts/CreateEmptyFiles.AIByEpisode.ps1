# PowerShell script to create empty files with specified filenames in the current directory

$filenames = @"
Star Trek - Deep Space Nine.S03E09.Defiant.mkv
Star Trek - Deep Space Nine.S03E10.Fascination.mkv
Star Trek_ Deep Space Nine - s04e01 - The Way Of the Warrior.mp4
Star Trek_ Deep Space Nine - s04e03 - The Visitor.mp4
Star Trek_ Deep Space Nine - s04e04 - Hippocratic Oath.mp4
Star Trek_ Deep Space Nine - s04e09 - The Sword Of Kahless.mp4
Star Trek_ Deep Space Nine - s04e10 - Our Man Bashir.mp4
Star Trek_ Deep Space Nine - s04e11 - Homefront.mp4
Star Trek_ Deep Space Nine - s04e12 - Paradise Lost.mp4
Startrekds9.s03e11.mkv
Startrekds9.s03e12.mkv
Startrekds9.s03e13.mkv
Startrekds9.s03e14.mkv
Startrekds9.s03e15.mkv
Startrekds9.s03e16.mkv
Startrekds9.s03e17.mkv
Startrekds9.s03e18.mkv
Startrekds9.s03e19.mkv
Startrekds9.s03e20.mkv
Startrekds9.s03e25.mkv
Startrekds9.s03e26.mkv
Startrekds9.s04e05.mkv
Startrekds9.s04e06.mkv
Startrekds9.s04e07.mkv
Startrekds9.s04e08.mkv
Startrekds9.s04e13.mkv
Startrekds9.s04e14.mkv
Startrekds9.s04e15.mkv
Startrekds9.s04e16.mkv
Startrekds9.s04e17.mkv
Startrekds9.s04e18.mkv
Startrekds9.s04e19.mkv
Startrekds9.s04e20.mkv
Startrekds9.s04e21.mkv
Startrekds9.s04e22.mkv
Startrekds9.s04e23.mkv
Startrekds9.s04e24.mkv
Startrekds9.s04e25.mkv
Startrekds9.s04e26.mkv
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