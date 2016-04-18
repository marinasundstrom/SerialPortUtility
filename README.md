# SerialPortUtility

Serial Port Utility is a small utility program for communicating via serial ports (COM ports).

The program is essentially meant to be a free basic replacement for the old an retired HyperTerminal (last release with Windows XP).

The program targets .NET Framework 4.5 (supported on Windows 7, 8 and 10)

![alt text](https://github.com/robertsundstrom/SerialPortUtility/blob/master/Images/Terminal.png "Serial Port Utility: Terminal")

## Project

The code itself serves as a reference implementation of the Model-View-ViewModel (MVVM) design pattern.

The application is as of yet still an unfinished work in progress. Please post any bugs and requests under the Issues section.

Check the develop branch for the latest version.

## Build the solution

1. Clone (or download) the repository:

	```sh
	git@github.com:robertsundstrom/SerialPortUtility.git
	```

2. Open the solution in Visual Studio 2015 (or later).
3. Rebuild the solution. Will restore NuGet packages automatically.
4. Run the program.

## Testing

You can install software that provides virtual COM Ports for testing (and development) when no serial ports or devices are available. With two interconnected virtual COM ports you can easily run the Serial Port Utility against, for example, PuTTY.

Go to https://code.google.com/archive/p/powersdr-iq/downloads and download "Com0Com v3.0.0.0". Install it and you are good to go.