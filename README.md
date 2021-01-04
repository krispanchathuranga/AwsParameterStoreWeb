@@ -1,2 +1,75 @@
# AwsParameterStoreWeb
A simple solution to retrieve and filter AWS Parameter store values
A simple solution to query AWS System Manager : Parameter Store

The repository contains an ASP.&#8203;NET Core web application generated using Visual Studio and runs on ASP.NET Core 3.1. 

---

## Table of Contents

* [Features](#features)
* [Used Technology Stack](#used-technology-stack)
* [Prerequisites](#prerequisites)
* [Getting Started](#getting-started)

## Features

* Display both Key and Value
* Easy UI filtering, 
* Easy parameter update

## Used Technology Stack

**ASP.NET Core 3.1:**

* [AWSSDK.SimpleSystemsManagement](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/SSM/TSSMClient.html)
* UI LIbrary - [jQuery DataTables](https://datatables.net/)

## Prerequisites

* [.NET Core](https://www.microsoft.com/net/download/windows) >= 3.1
* Code editor VS Code, or VS 2017/19

---

## Getting started

Clone this repository `git clone https://github.com/krispanck/AwsParameterStoreWeb`

#### Locate the AWS credential file

update the `appsettings.Development.json` file with your aws profile and crecential file location.

    "AWS": {
        "Profile": "default",
        "Region": "eu-west-1",
        "AWSProfilesLocation": "E:\\credentials"
      }


#### Run the application

You have three choices when it comes to how you prefer to run the app. You can either use the command line or the build-in run command.

### 1. Using the command line

* Run the .NET application using `dotnet run`

### 2. Using the built-in run command

* Run the application in VSCode or Visual Studio 2017 by hitting `F5`

> It will take some time during the first run to download all client side dependencies.

Browser will be opened automatically


## Publish the application 
For easy access application can be deployed to local IIS. 

### 1. Folder output

* Run the .NET publish command using Release configuration: `dotnet publish -c Release`

or

* Follow the Publish wizard in Visual Studio selecting Folder profile.

---

## Issues and Contribution

Want to file a bug, contribute some code, or improve documentation? Excellent! Please make sure to check existing issues before opening a new one.

---