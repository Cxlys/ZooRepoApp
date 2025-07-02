# ZooRepoApp

> A C# console application for managing a zoo, built as a learning and demonstration project for C# and .NET design, specifically object-oriented (OOP) principles and interface-based design.

## About
This application simulates a basic zoo management system built in C# as a console app. It showcases:
- Object-oriented design
- Interfaces
- Menu-driven UI for user interaction
- Clean code practice

It is intended to be a learning and demonstration tool for my **C# and .NET design**.

I am also using it as a springboard to learn **Git** development on the command line. **All commits to this project have been made in the Linux terminal via Bash commands**.

## Features
- You can create and manage different types of animal pens, of which each pen consists of one type of animal each (e.g. Pen\<Lion>, Pen\<Tiger>)
- You can add new animals with a name and age
- Navigate menus of the application using keyboard input
- Strong separation of concerns (Zoo, ZooRepository, Animal, Pen) etc. grouped into clean interfaces (IMenuable, IListlike)

## Tech Stack
- **Language**: C# (.NET 8)
- **Runtime**: Console application
- **IDE**: Visual Studio Code
- **Target OS**: Cross-platform (however tested on Linux)

## How to Run
1. Clone the repository
```bash
git clone https://github.com/yourusername/zoo-console-app.git
cd zoo-console-app
```
2. Run the application
```bash
dotnet run
```

Ensure that you have .NET 8 SDK installed. This can be checked using `dotnet --version`.

## Example Output
```
Welcome to the Zoo app!

Please select an option:
1. List all pens
2. Select a pen
X. Exit the application
```

## Design Principles
This project looks to demonstrate several key programming concepts, such as polymorphism (through overriden animal behaviour), abstraction (with core methods exposed with interfaces), encapsulation (by hiding zoo logic in the ZooRepository class) and inheritance (via the hierarchy of Animal types). 

It also shows a strong separation of concern, with each part of the process (Zoo -> Main Menu, ZooRepository -> Pens Menu, Pen -> Animals Menu).,