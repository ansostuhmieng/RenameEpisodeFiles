# PowerShell script to create empty files with specified filenames in the current directory

$filenames = @"
Star Trek Deep Space Nine.S03E09.Defiant.mkv
Star Trek Deep Space Nine.S03E10.Fascination.mkv
Star Trek Deep Space Nine.S03E11.Past Tense (1).mkv
Star Trek Deep Space Nine.S03E12.Past Tense (2).mkv
Star Trek Deep Space Nine.S03E13.Life Support.mkv
Star Trek Deep Space Nine.S03E14.Heart of Stone.mkv
Star Trek Deep Space Nine.S03E15.Destiny.mkv
Star Trek Deep Space Nine.S03E16.Prophet Motive.mkv
Star Trek Deep Space Nine.S03E17.Visionary.mkv
Star Trek Deep Space Nine.S03E18.Distant Voices.mkv
Star Trek Deep Space Nine.S03E19.Through the Looking Glass.mkv
Star Trek Deep Space Nine.S03E20.Improbable Cause.mkv
Star Trek Deep Space Nine.S03E25.Facets.mkv
Star Trek Deep Space Nine.S03E26.The Adversary.mkv
Star Trek Deep Space Nine.S04E01.The Way of the Warrior (1).mp4
Star Trek Deep Space Nine.S04E03.The Visitor.mp4
Star Trek Deep Space Nine.S04E04.Hippocratic Oath.mp4
Star Trek Deep Space Nine.S04E05.Indiscretion.mkv
Star Trek Deep Space Nine.S04E06.Rejoined.mkv
Star Trek Deep Space Nine.S04E07.Starship Down.mkv
Star Trek Deep Space Nine.S04E08.Little Green Men.mkv
Star Trek Deep Space Nine.S04E09.The Sword of Kahless.mp4
Star Trek Deep Space Nine.S04E10.Our Man Bashir.mp4
Star Trek Deep Space Nine.S04E11.Homefront.mp4
Star Trek Deep Space Nine.S04E12.Paradise Lost.mp4
Star Trek Deep Space Nine.S04E13.Crossfire.mkv
Star Trek Deep Space Nine.S04E14.Return to Grace.mkv
Star Trek Deep Space Nine.S04E15.Sons of Mogh.mkv
Star Trek Deep Space Nine.S04E16.Bar Association.mkv
Star Trek Deep Space Nine.S04E17.Accession.mkv
Star Trek Deep Space Nine.S04E18.Rules of Engagement.mkv
Star Trek Deep Space Nine.S04E19.Hard Time.mkv
Star Trek Deep Space Nine.S04E20.Shattered Mirror.mkv
Star Trek Deep Space Nine.S04E21.The Muse.mkv
Star Trek Deep Space Nine.S04E22.For the Cause.mkv
Star Trek Deep Space Nine.S04E23.To the Death.mkv
Star Trek Deep Space Nine.S04E24.The Quickening.mkv
Star Trek Deep Space Nine.S04E25.Body Parts.mkv
Star Trek Deep Space Nine.S04E26.Broken Link.mkv
"@ -split "`n"

$subfolder = "TestFiles"

foreach ($filename in $filenames) {
    $trimmed = $filename.Trim()
    if ($trimmed) {
        $filepath = Join-Path $subfolder $trimmed
        # Check if the file exists and report
        if (Test-Path $filepath) {
              Write-Host `u{2705} + "Successfully Renamed: $filepath"
        } else {
              Write-Host "`u{274C} File does not exist: $filepath"
        }
    }
}