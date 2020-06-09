# New User Track Request
- User visits request page
- Page includes description and link to initiate oauth
- Oauth link redirects to twitch
- Twitch returns to authorize endpoint
    - Store authorization credentials
    - Request oauth key
    - Store key and refresh key
    - Get user id
    - Place webhook registration request in background queue scheduled to fire asap
    - Return view notifying user that they have been registered for follow tracking

### Notes:
Users may not request follow tracking for anyone except themself. This is to prevent abuse.