# Pinger 

Pinger is compatible with Windows 11 and Kubuntu 26.04. It should also work on Ubuntu 26.04 and Ubuntu-based distributions.

## Device Configuration

Each monitored device requires the following parameters:

- **Name**
- **IP address**

Duplicate device names and IP addresses are not allowed.

## Application Controls

- **Double-click** a device with the left mouse button to edit its parameters.
- **Right-click** a device to remove it.
- Click **Save** to store the configured devices in the following file:

  - **Windows 11:** `C:\\Users\\<user>\\AppData\\Roaming\\Pinger\\settings.json`
  - **Kubuntu 26.04:** `/home/<user>/.config/Pinger/settings.json`

The application attempts to load this file when it starts.

## Email Settings

- **SMTP Host** — the address of the SMTP server.
- **SMTP Port** — the port used by the SMTP server.
- **Mail Sender** — the email address from which the application sends notifications.
- **Mail Receiver** — the notification recipient. This can be either an individual email address or a distribution group.

## Ping Settings

- **Ping Timeout (ms)** — how long a single ping waits for a response.
- **Ping Interval (s)** — how many seconds the application waits after one ping finishes before sending the next one.
- **Fails to Notification** — the number of consecutive failed pings required before the application sends a notification.
- **Notification Interval (minutes)** — how often the application sends a reminder while a device remains offline.

Yes: I formated readme with AI - app done and tested by me with help (search engine) of AI 
