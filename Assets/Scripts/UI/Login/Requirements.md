## Requirements

- User input.
    - User is able to enter a username of their choice within a username input field.
    - User is able to submit the entered username by pressing a Submit button.
- Invalid username.
    - If username does not fit the requirements:
        - May only contain letters
        - Be between 3 characters and 15 characters (inclusive)
    - Then user will see appropriate validation messages for each of the failed requirements.
- Duplicate username.
    - If username already exists on the server, then user will see “Username is taken” validation message.
- Successful sign-in.
    - User should be able to see `Signed in as <username>` in the bottom right to indicate the user’s entered username.
