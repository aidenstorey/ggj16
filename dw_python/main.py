# Imports
import twitch
import keypresser
import os

# Initialise integrations.
t = twitch.Twitch()
k = keypresser.Keypresser()

# Get environment variables.
username = os.environ.get('USERNAME', '')
oauth_key = os.environ.get('OAUTH_KEY', '')

# Attempt to connect to twitch stream.
t.twitch_connect(username, oauth_key)

# Infinite loop to process messages.
while True:
    # Pull new messages from twitch.
    new_messages = t.twitch_recieve_messages()

    # Check that there is a new message.
    # True - Check if message is one of the current signals.
    if new_messages:
        # Iterate over all of the new messages.
        for message in new_messages:
            # Store the message data.
            msg = message['message'].lower()
            username = message['username'].lower()

            # Check that the mess is up.
            # True - Simulate up press.
            if msg.lower() == 'up':
                print('up')
                k.key_press('{UP}')
            # Check that the mess is down.
            # True - Simulate up press.
            elif msg.lower() == 'down':
                k.key_press('{DOWN}')
            # Check that the mess is left.
            # True - Simulate up press.
            elif msg.lower() == 'left':
                k.key_press('{LEFT}')
            # Check that the mess is right.
            # True - Simulate up press.
            elif msg.lower() == 'right':
                k.key_press('{RIGHT}')
            # Check that the mess is a.
            # True - Simulate up press.
            elif msg.lower() == 'a':
                k.key_press('q')
            # Check that the mess is b.
            # True - Simulate up press.
            elif msg.lower() == 'b':
                k.key_press('w')
            # Check that the mess is start.
            # True - Simulate up press.
            elif msg.lower() == 'start':
                k.key_press('1')
            # Check that the mess is select.
            # True - Simulate up press.
            elif msg.lower() == 'select':
                k.key_press('2')
