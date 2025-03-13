# Mailpit Overview  

Mailpit is a lightweight email testing tool that provides a web-based interface for capturing and inspecting emails sent from applications in a development or testing environment. It acts as a local SMTP server and webmail client, making it easy to view and debug outgoing emails without sending them to real recipients.  

## Usage with the Provided Docker Compose Setup  

The **Docker Compose** configuration runs Mailpit as a service with the following settings:  

- **SMTP Server on Port 1025**: Applications can send emails via Mailpit by configuring their SMTP server to `mailpit:1025`.  
- **Web Interface on Port 8025**: Developers can access `http://localhost:8025` to view and inspect captured emails.  
- **Persistent Storage**: Emails are stored in a volume (`mailpit-data`), ensuring they persist across container restarts.  
- **Authentication Flexibility**: The configuration allows Mailpit to accept any SMTP authentication and permit insecure authentication, making it easy to integrate with development setups.  
- **Message Limits**: The setup limits stored messages to **5000** to manage storage efficiently.  

## Practical Use Cases  

- Testing email notifications in a local development environment.  
- Debugging email formatting, headers, and content.  
- Ensuring email sending functionality works correctly before deploying to production.  