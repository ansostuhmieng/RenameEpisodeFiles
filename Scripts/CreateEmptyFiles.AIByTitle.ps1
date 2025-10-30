# PowerShell script to create empty files with specified filenames in the current directory

$filenames = @"
DirtyJobs.AerialTramWorker.mkv
DirtyJobs.AlaskaWildGooseChaser.mkv
DirtyJobs.AnimalBarber.mkv
DirtyJobs.DairyCowMidwife.mkv
DirtyJobs.ElevatorRepairman.mkv
DirtyJobs.ExoticAnimalKeeper.mkv
DirtyJobs.FloatingFishFactory.mkv
DirtyJobs.FuelTankRemover.mkv
DirtyJobs.ReefBallMaker.mkv
DirtyJobs.SteelMillWorker.mkv
DirtyJobs.WindFarmTechnician.mkv
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