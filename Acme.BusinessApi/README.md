# Xlent.Lever.TemplateProject
Basic project for demonstration/test purposes.
Inversion of Control(IoC), a loosely coupled solution architecture, regressiontests, logging, how to use Xlent.Lever classes, WebApi tricks and implementations are all demonstrated here. 

## Solution Architecture
![Solution Architecture Basic image](https://raw.githubusercontent.com/xlent-fulcrum/Xlent.Lever.TemplateProject/master/SolutionArchitectureBasic.png?raw=true "Solution Architecture Basic")

Domain interfaces and models that are the core of the application are stored in the Business Logic Layer (BLL). Communication from and to the BLL is done using these interfaces and models. Models and interfaces specific for the Service Layer (SL) or Data Access Layer (DAL) are stored in each of those projects separately and are only known to those projects.

The SL is responsible for registering and resolving dependencies in the SL, BLL and DAL by using IoC.
