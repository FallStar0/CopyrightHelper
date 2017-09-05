# CopyrightHelper
A visual studio plugin which use for developers who need to quick insert copyright info to code file.
Current support : VS2013,VS2015,VS2017.


## Origin
As a good habit for developers , we need to insert some of the copyright and descriptions when we create a new code file.

Exporting templates is not convenient because you have to do it for all kinds of files.

So I create this to resolve the problem, insert different copyright info in different file types.


## Usage
![shot](/CopyrightHelper/Resources/shot1.en.png "shot")

Open VS menu->Tools-> Config CopyrightHelper
Add file extension like *.cs , and set the insert content like :

```
//===================================================
//  Copyright @  @company @year
//  Author : @yourname
//  Time : @time
//  Description : 
//===================================================
```

Here are some useful pre-define symbols (case sensitive):
```
@yourname
@company
@year
@time
```
You can config these field in the config panel.

You can set if the content need to insert to the top of the file.


By the way , *.* match all the files.

The buttom one has the lowest priority(*.* by default).

## License
[Apache Licene 2.0](/LICENSE "Apache Licene 2.0")