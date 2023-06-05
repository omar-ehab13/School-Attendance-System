# School-Attendance-System
School Attendance System is manage students attendance information in school. It help school administrative to generate automatically reports about attendance for several periods or classes, also help parent to show its children attendance report or notification when his student has arrived the school. 

# Drawbacks in the code
This is the first project for me so I start with some uncleared concepts then while working on the projct I learn new and new.
All issues on my code I will work in it.

- Repository pattern in DAL layer: my implementation for repository pattern is bad I can abstract more methods: make find with expression and array of includes.
- DTO in BLL layer: now if I work in DTO I will make folder for each service to put its all DTO. in the first of project I start with two folder request and response but latter I make the resposne generic so it is not important to put in any DTO more. My name for DTO not best thing some I start with Model extension then I used Dto extension
- Seeding data: I wanted to start the project with default user "SuperAdmin" and want to seed roles also, so I use empty migration then seed the data in migration directly. I think the best is to make folder in DAL layer called configuration and each entity has its configuration and can seed initial data in it. 
- Demo data in Data folder in DAL layer. I think it also not right think, I was want to add some fake data using Bogus library but the right way I think in another structure
- There is no unit test: I want to add layer for testing my services
- Bad design Services in BLL layer: in login I want to return data for each user and this data not in the same model so I made bad design and tightly couple design between users services and account service then I make FactoryService to overlap the circular dependancy injection: it is really bad design and I will work to refactor it.
- Some endpoints routes are in unconvient manner: like: [GET] api/parents/all, the best is [GET]: api/parents 
