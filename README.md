# LpFileParser

This is a C# library to parse [LP files](http://lpsolve.sourceforge.net/5.1/lp-format.htm).

## Usage

Usage is simple:

```
var reader = new LPFileReader();
var lpfile = reader.FromFile(@"c:\test.lp");

// You can access all parts of the problem now via properties, e.g.:
Console.WriteLine(lpfile.Objective);
```