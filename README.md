# azure-ad-auth

1. Register an app in azure ad using the below command 
    az ad app create --display-name adauthdemo --reply-url https://localhost:3000
    
    go to application and update add the client secret and add the API permissions for the below resources:
    * Microsoft Graph
    * Azure Service Management
    * Azure Active Directory Graph
    
 2. Clone the repo and update the keys in web.config files 
 
 3. Make to  update the application url properly in app registration and web.config both(same)
 
 3. Build and run (Ctrl+F5) 

 4. It will redirect application to microsoft login page and post login successful it will return to your home page.
