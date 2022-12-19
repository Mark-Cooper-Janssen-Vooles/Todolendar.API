## API for Todolender App 

Todolender is an calendar + todo combination application. Features include signing up, editing your profile, as well as creating todo's and adding them into a calendar. 

### Planning 
Figma designs: 
https://www.figma.com/file/ona2QoEu6QzTcyffAervOy/Todolender?node-id=0%3A1&t=KPdD8o2qc6cbYQnZ-0

DB Schema: 
https://app.diagrams.net/#G1NYqMTprbHGnyYW-6s-Pc1sLVT3hZQu_x

### Tech 

This project is created as a .net web api, using entity framework / swagger / fluent validations / automapper / Json Web Token.

### Patterns 

This project uses dependency injection and the repository pattern. It is an attempt at onion architecture.

https://www.codeguru.com/csharp/understanding-onion-architecture/

The domain layer can be found in the Models folder, while the repository layer can be found in the repositories folder and the services layer can be found in the controllers folder. 