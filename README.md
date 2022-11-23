# Drake
## The name
This is a web interface for Ugly Duckling, Detectifys module test application.  
I wanted a name that was a synonym with duck and found "Drake".  
I then realized that Drake Mallard (the alter-ego of Darkwing Duck) is actually called Drake for that reason =D.
It just happens to be that "Drake" also means dragon in Swedish so the name was perfect.


## What is it?
A web interface for Ugly Duckling.
My wife started her new role as the Community Manager for Detectify.  
To understand more of what she was doing (since I don't have a background in cyber security) I decided to learn more by writing some code.  
Detectify launched a new tool called Ugly Ducking where ethical hackers can build and test their modules before submitting them.  
The format is a JSON format that describes the request and what to test in the response to identify a possible threat.  
Read more here https://labs.detectify.com/2021/05/18/detectify-research-team-releases-ugly-duckling-a-web-scanner-for-ethical-hackers/
To make this run I would have to install Go and learn how that works (which is something I wasn't too keen to do).  
So I thought would it be possible to make a Blazor implementation of this?

Since Microsoft has released a version of Monaco (the engine VSCode is running) I wanted to give that a try.
I also added a JSON schema that can be used in the web implementation or directly from VSCode to get full intelisence when editing modules.
The goal of the project is to make it easier to write and test modules for Ugly Duckling.
## Usage
There are two ways to use this application.
1. We can use the online version at https://engstromjimmy.github.io/drake with an version of Monaco (VSCode).  
This is the easiest way to get started. 
Since it is JavaScript making the calls we need to install a plugin in the Web browser to be able to make cross-site calls.
But if you want to test it without installing anything you can test the default module with the URL "https://engstromjimmy.github.io/Drake/".

2. We can use VSCode directly by simply refering to the JSON Schema.
```
{
  "$schema": "https://raw.githubusercontent.com/EngstromJimmy/Drake/main/ModuleSchema.json",
  "response": {
    "matches": [
      {
        "type": "status",
        "code": 200
      },
      {
        "name": "Hello World",
        "type":"regex"
      }
    ]
  }
}
```
