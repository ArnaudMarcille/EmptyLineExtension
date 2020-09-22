

# EmptyLine Extension

  

Visual Studio extension for remove empty lines (more than one consecutive empty line)

[![Build Status](https://my-biblipi.visualstudio.com/Plugins/_apis/build/status/EmptyLine%20Extension%20build?branchName=master)](https://my-biblipi.visualstudio.com/Plugins/_build/latest?definitionId=3&branchName=master)
  

## Getting Started

  

The extension is available [the marketplace](https://marketplace.visualstudio.com/items?itemName=Mybiblipi.EmptyLineExtention) or directly in Visual studio in *Tools => Extension and Update*

[![marketplace](https://img.shields.io/static/v1?label=Marketplace&message=1.2.0&color=green)](https://marketplace.visualstudio.com/items?itemName=Mybiblipi.EmptyLineExtention)
 
## How to use it ?

  

### Manual

  

Once the extension is installed, a "Remove empty lines" option is available when right clicking in a file.

  

![Image](/images/readMe_Option_01.png)

  

If there is some selected text, the selection will be the only part of the document that will be processed.

  

### Automatic

  

There is now an option in Visual Studio for auto-reformat the code:

you can fint it in *Tools => Options => EmptyLine Extension*

  
### Configurations

With the version 1.1.0, you can edit the number of allowed lines, for that go on "*Tools => Options => EmptyLine Extension*", and edit the option "Allowed lines", by default there is no values, in this case, 1 line is kept, if you didn't want any line, then set value to 0. All numbers are allowed for this option.

## Ignore first line

Since version 1.3.0, you can choose to ignore the first lines of the file: if the first line is empty, all the following empty lines will not be removed. But at the first line who contains text, the seetings will be applied.
![Image](/images/readMe_Settings_02.png) 

you can add multiples rules, and manage them as you want. 

## Regex

Since version 1.3.0, you can now add some specific rules in regex. The regex will be apply to the file full path (path and file name).
See below an exemple who will manage all the json file, and set the number of tolerated line to 0:
![Image](/images/readMe_Settings_01.png) 

you can add multiples rules, and manage them as you want. 

### Prerequisites

  

The extension is only available Visual Studio 2017 and 2019.
