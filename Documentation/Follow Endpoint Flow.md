# Follow Endpoint Hit
- Fetch user guid from query string.
- Fetch json from response body.
- Fetch secret guid from json.
- If secret guid doesn't match submitted for user,
    - Log event
    - Return Ok
    - Terminate here.
- Save to database
    - Get database id for requesting user from guid.
    - Save user id, datetime added, endpoint type, and json to table.