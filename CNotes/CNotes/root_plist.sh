#!/bin/bash

# c0lby:

## Create New Root.plist file ##

PreparePreferenceFile

		AddNewTitleValuePreference  -k "VersionNumber" 	-d "$versionNumber ($buildNumber)" 	-t "Version"

		AddNewTitleValuePreference  -k "GitCommitHash" 	-d "$gitCommitHash" -t "Git Hash"


	AddNewPreferenceGroup	-t "Nonsense"
		AddNewStringNode 	-e "FooterText" 	-v "$copyright"

	AddNewToggleSwitchPreference -k "ResetState" 	-d false 	-t "Reset"