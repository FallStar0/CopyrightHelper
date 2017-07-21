# CopyrightHelper
A plugin for visual studio than can insert copyright info to code file

## 起源
由于每次新建代码文件都会有需要插入一些公共的版权信息，而导出模板方式不是很方便，而且要各种类型都要导出，这样就比较麻烦了。

所以该工具就也因此出现，达到根据文件类型来插入对应的版权信息。


## 使用
这是一个VS插件项目，该插件主要用于帮助大家根据不同的代码文件类型，插入对应的版权信息，如

*.cs 就代表一般的 C#代码文件，它的默认插入版权信息为：
```
//===================================================
//  Copyright @  @company @year
//  作者：@yourname
//  时间：@time
//  说明：
//===================================================
```

当前可用预定义符号为（大小写敏感）:
```
@yourname
@company
@@year
@time
```

另外*.* 类型代表匹配所有文件。


你可以通过 VS菜单->项目->配置CopyrightHelper，来打开配置菜单进行配置。

你可以配置@yourname与@company等，还有对应的模板。

另外你可以指定该插入信息会插入到代码文件头部。

优先级最低的在最后一项（一般是 *.* ）。
