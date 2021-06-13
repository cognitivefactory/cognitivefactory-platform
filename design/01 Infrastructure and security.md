# Infrastructure and Security

## Concepts

**Cognitive Platform** : Fully integrated set of tools designed to help *companies* build, deploy and maintain cognitive solutions at scale, using an industrialized and secure workflow.
- Companies already run very rich and complex IT systems, mostly made of traditional computer programs working on structured data
- The advent of more efficient machine learning techniques gives them an opportunity to automate new tasks, by extending the reach of the IT system to unstructured data (text, images, voice) and more sophisticated predictions
- To stay competitive, companies need to introduce a new type of component in their applications : models trained by example
- The methods, workflows, tools needed to build and maintain this new family of components are quite different from the traditional developer toolset : many new capabilities need to be built at once, and this is really hard in a corporate environment
- When building cognitive solutions at the scale of a company, it is also very important to be able to share and reuse a lot of elements between projects : for example, all projects will work on the same company language, the company will want to know what customers said about a specific topic on all channels, etc ...
- Fairness, robustness, auditability are difficult issues with trained models, a lot of regulations apply to "AI solutions" (for example GDPR) : these requirements can only be satisfied if they are embedded by design in a end to end workflow
- That's why companies need a single integrated platform to manage their cognitive models end to end : working with custom solutions for each application is not economically viable
- The goal of the Cognitive Platform to provide ...

**Clusters and Namespaces** : All elements of the self-contained Cognitive Platform 
- Kubernetes Objects
- Region, zones

**Platform Environment** : Set of Kubernetes Objects
- Each environment is deployed as a separate Kubernetes namespace
- If regional disaster recovery is not required, this namespace can be deployed on a 
- If regional disaster recovery two

**Platform Installation** : Set of Platform Environments managed by a *a single IT operations team*. 
- For example : all environments managed by a transversal IT organization inside a multi-companies corporate group.
- A specific *Platform Installation Operations Environment* is associated with a *Platform Installation*
- This unique environment centralizes the IT monitoring inforomations and the low level management operations for all the other environments

**Tenant Instances** : A Platform Installation is a collection of strictly separated Tenant Instances. 
- *No data should be shared* between two Tenant Instances.
- Each Tenant Instance can have *distinct scale, security and reliability requirements*.
- For example : each company inside the corporate group is a distinct tenant with its own confidential data and specific policies.
- Only the transversal IT operations team can span Tenant Instances (by definition).

**Lifecycle Environments** : Each Tenant Instance is divided in *4 isolated environments* dedicated to specific steps of the cognitive solutions lifecycle : *dev, train, uat, prod*. The role of each environment is decribed below :
- *dev* : used by IT developers to write and test traditional code for algorithms, components, services, and pipelines (for example : Python, C#).
- *train* : used by business experts to define busines concepts, annotate datasets and logs with labels, train models based the algorithms and pipelines written in the dev environment, and evaluate their performance.
- *uat* : used by future users to perform user acceptance tests on the trained solutions, by executing the end to end business process.
- *prod* : production environment with high availability and strict data isolation - no IT team member can ever see the raw data, the only environment where personal data is allowed.

**Platform Workspaces** : Each 

**Cognitive Solutions** :


**Builders and Users**
- **Builders** are all the experts who collaborate to build and improve a cognitive solution
  - Builders are organized in **Teams** and *Teams of Teams*
  - A **Role** is a set of **Platform Actions** a *Team* is allowed to perform
  - *Roles* are granted to *Teams* only, not individuals, because you should almost always make sure you have a backup for all roles
  - *Roles* are assigned to *Teams* only in the context of *User Solutions*, 
- **Users** are all the clients or employees of the company who are allowed to use a cognitive solution

**Projects**
  - A **Project** is used to track the *initial creation* or a *continuous improvement phase* of a cognitive solution
  - Each *Project* has mandatory *start and end dates* and should have *Business Goals*
  - A **Business Goal** is a set of *Key Indicators* you should measure to make sure your project is a success
  - **Resources** are the artefacts used in the process of building a cognitive solution are different kind of **Resources** : data, models, code, metadata ...
  - *Teams* work on *Projects*
  - *Resources* can only be browsed, modified and deployed in the context of a *Project*
  - acces ritghs are granted  