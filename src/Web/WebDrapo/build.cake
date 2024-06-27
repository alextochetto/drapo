﻿#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#addin nuget:?package=Cake.Incubator&version=7.0.0
#addin nuget:?package=Microsoft.Extensions.DependencyInjection.Abstractions&version=2.1.1
#addin nuget:?package=Microsoft.Extensions.DependencyInjection&version=2.1.1
#addin nuget:?package=dotless.Core&version=1.6.7

var target = Argument("target", "Default");

Task("Debug")
    .Does(() =>
{
     var fileContent = "";
     fileContent = fileContent + "\n" + FileReadText("../../Middleware/Drapo/node_modules/es6-promise/dist/es6-promise.auto.min.js");
     fileContent = fileContent + "\n" + FileReadText("../../Middleware/Drapo/node_modules/@microsoft/signalr/dist/browser/signalr.min.js");
     IEnumerable<FilePath> files = GetFiles("../../Middleware/Drapo/js/*.js");
     foreach(FilePath file in files)
       fileContent = fileContent + "\n" + FileReadText(file);
     CreateDirectory("../../Middleware/Drapo/lib");
     FileWriteText("../../Middleware/Drapo/lib/drapo.js", fileContent);
     CreateDirectory("../../Web/WebDrapo/node_modules/@types/drapo");
     CopyFile("../../Middleware/Drapo/lib/drapo.js","../../Web/WebDrapo/node_modules/@types/drapo/drapo.js");
});

Task("Release")
    .Does(() =>
{
     var fileContent = "";
     fileContent = fileContent + "\n" + FileReadText("../../Middleware/Drapo/node_modules/es6-promise/dist/es6-promise.auto.min.js");
     fileContent = fileContent + "\n" + FileReadText("../../Middleware/Drapo/node_modules/@microsoft/signalr/dist/browser/signalr.min.js");
     IEnumerable<FilePath> files = GetFiles("../../Middleware/Drapo/js/*.js");
     foreach(FilePath file in files)
       fileContent = fileContent + "\n" + FileReadText(file);
     //TODO: Uglify fileContent here
     CreateDirectory("../../Middleware/Drapo/lib");
     FileWriteText("../../Middleware/Drapo/lib/drapo.min.js", fileContent);
     CreateDirectory("../../Web/WebDrapo/node_modules/@types/drapo");
     CopyFile("../../Middleware/Drapo/lib/drapo.min.js","../../Web/WebDrapo/node_modules/@types/drapo/drapo.min.js");
});

Task("Type")
    .Does(() =>
{
     var fileContent = "";
     IEnumerable<FilePath> files = GetFiles("../../Middleware/Drapo/js/*.d.ts");
     foreach(FilePath file in files){
        if (!string.IsNullOrEmpty(fileContent))
            fileContent = fileContent + "\n";
        fileContent = fileContent + FileReadText(file);
     }
     CreateDirectory("../../Middleware/Drapo/lib");
     FileWriteText("../../Middleware/Drapo/lib/index.d.ts", fileContent);
     CreateDirectory("../../Web/WebDrapo/node_modules/@types/drapo");
     CopyFile("../../Middleware/Drapo/lib/index.d.ts","../../Web/WebDrapo/node_modules/@types/drapo/index.d.ts");
});

Task("Lint")
    .Does(() =>
{
    //TODO: We need to check for ts lint here
});

Task("Default")
    .IsDependentOn("Lint")
    .IsDependentOn("Release")
    .IsDependentOn("Debug")
    .IsDependentOn("Type");

RunTarget(target);