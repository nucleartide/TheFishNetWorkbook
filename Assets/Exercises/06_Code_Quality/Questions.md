> For this set of exercises, the goal is to define what code quality means as you implement networked gameplay features, and to enforce that definition in your growing lobby system codebase.

## Exercises

### Question 1: Project Structure

If you haven't already, create a project directory structure whose mental model best makes sense to your brain.

Note that you can organize things however you wish. The only criterion is that you are able to quickly identify where to fix any particular problem.

<details><summary>Answer</summary>

My own project structure (which you can observe within this `06_Code_Quality/` directory) is organized by application concerns.

Currently, we deal primarily with 3 application concerns:

1. `Client/`: Scripts and behaviors relating to the game's client. Can be considered the "backend" to the UI.
2. `Lobby/`: Scripts that implement this project's lobby system. Can be considered the "backend" to the UI.
3. `UI/`: Scripts that implement the management of the on-screen UI.

And within `UI/`, there are directories that capture the notion of different user flows:

1. `Global/`: UI that is global to the application, such as fullscreen or popup alert messages.
2. `Login/`: UI that is specific to the application's login flows.
3. `NetworkManager/`: UI that is specific to the management of the application's network connections (the Server and Client buttons).

Your project can be organized completely differently. Whatever works for your brain!
</details>

### Question 2: Quality Assurance through User Stories

Write a set of requirements that together define the correct behavior for the *network management* portion of your application.

Then, go through each of the requirements, and verify that each requirement is satisfied in a running build of your application.

Finally, maintain this set of requirements in the codebase as a living document. (I opted to create a `NetworkManager/Requirements.md` file.) It should remain helpful as a checklist that can be used when needed, as well as something to modify to keep it up-to-date and useful.

<details><summary>Answer</summary>

Below are example requirements for network management:

1. Inactive state.
    1. UI should say “Waiting for player to connect” if Client connection is inactive.
2. In progress state.
    1. When either the Server or Client connections are attempting to connect, the Server / Client status indicators (on the buttons) should be yellow.
3. Failure state.
    1. If the Server fails to connect (perhaps because a device has already been designated as the Server), the UI should show a timed popup message: `Failed to start server (server may already exist)`
    2. If the Client fails to connect (perhaps because a Server has not been designated or established), the UI should show a timed popup message: `Failed to start client (may need to start server first)`
    3. If the Client fails to connect (perhaps because the Client is not on the same version as that of the designated Server), the UI should show a `Your executable is out of date. Please update to the latest version to proceed.` message.
4. Connected state.
    1. Both status indicators will be green.
    2. Also, the fullscreen "Waiting for player to connect" message should be empty.
</details>

### Question 3: Authentication Requirements

Like in [Question 2](#question-2-quality-assurance-through-user-stories), do the same for the login feature implemented in `05_Auth/`:

1. Write out a set of requirements that describe behavior.
2. Create a living document within an appropriate directory that will contain this set of requirements.
3. Create a build of your current application, and go through each of the requirements to verify that they are satisfied within your running build.

<details><summary>Answer</summary>

Below are example requirements for authentication:

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
</details>

### Question 4: Development Pipeline

Every developer has a *process*: a series of steps that they run through in order to implement and sign-off on a feature.

Let's codify that process by writing out the process's series of steps.

Doing so has the benefit of helping you consider where you are losing time, and where you may need to make improvements for the sake of developing more quickly.

<details><summary>Answer</summary>

One possible pipeline (that we've been following within this set of questions) is the following:

1. **Design.**
    1. **UI/UX design.** Sketch out (on paper or in [Figma](https://figma.com/)) what the feature should look like: its user flow, success, and error states.
    2. **Technical design.** Sketch out a waterfall diagram (see previous questions for examples) of how data flows through the application in order to implement the UI/UX design.
2. **Implementation.**
    1. **First draft.** Do a rough, first pass of the code implementation. The goal is to explore, learn, and simply get things working over creating perfectly clean and optimized code.
    2. **Final draft.** Once a working, first draft is established, go through the first draft and clean up the code so that it's maintainable and extensible long-term.
    3. **Quality assurance (QA).** Write out a list of requirements that describe correct behavior of your feature, maintain this list within a `Requirements.md` document, and run through the list to ensure that each of the requirements are satisfied within your implementation. You may need some back-and-forth between code and QA to get this right.
3. **Release.**
    1. Commit your changes to your main branch (if using Git), deploy your game (see the `04_Deployment/` chapter for exercises), and share your game with the world!
</details>
