## Requirements

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
