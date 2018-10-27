# Hue-CLI

Command line app for controlling Philips Hue Lights.

```
hue 1.0.0
Copyright 2018 Rob Prouse, All Rights Reserved

  -a, --alert         Set an alert on the lights.
  -b, --brightness    Set the brightness of a light, from 0 to 100 percent.
  -c, --color         Color as a HEX color in the format FF0000 or #FF0000, or a common color name like red or blue.
  -i, --ip            IP Address of the Hue Bridge. Will default to the first bridge found.
  -l, --light         The light to perform an action on. If unset or 0, all lights.
  -f, --off           Turn lights off.
  -o, --on            Turn lights on.
  -r, --register      Register with a Hue Bridge. Registration usually happens automatically, you should only need to use to fix a broken registration.
  -w, --wait          Wait before exiting.
  --bridges           List bridges on the network.
  --lights            List lights.
  --help              Display this help screen.
  --version           Display version information.
```

## Building

`dotnet publish -c Release -r win-x64`
