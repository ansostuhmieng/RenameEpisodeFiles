# PowerShell script to create empty files with specified filenames in the current directory

$filenames = @"
Dirty Jobs.S02E25.Fuel Tank Cleaner.mkv
Dirty Jobs.S03E08.Wild Goose Chase.mkv
Dirty Jobs.S03E12.Reef Ball Maker.mkv
Dirty Jobs.S03E13.Exotic Animal Keeper.mkv
Dirty Jobs.S03E18.Steel Mill Worker.mkv
Dirty Jobs.S03E25.Dirty Jobs of the Big Apple.mkv
Dirty Jobs.S03E26.Floating Fish Factory.mkv
Dirty Jobs.S03E27.Dairy Cow Midwife.mkv
Dirty Jobs.S03E28.Aerial Tram Greaser.mkv
Dirty Jobs.S03E31.Wind Farm Technician.mkv
Dirty Jobs.S03E32.Animal Barber.mkv
"@ -split "`n"

$subfolder = "TestFiles"

foreach ($filename in $filenames) {
    $trimmed = $filename.Trim()
    if ($trimmed) {
        $filepath = Join-Path $subfolder $trimmed
        # Check if the file exists and report
        if (Test-Path $filepath) {
            Write-Host "✅ Successfully Renamed: $filepath"
        } else {
            Write-Host "❌ File does not exist: $filepath"
        }
    }
}