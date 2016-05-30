# Black Desert Multi Tool

This is a small project created to add some convenience features to "Black Desert"

It is basically just a transparent WPF application laid on top of the game's client. It uses PInvoke to get information on the game's window-dimensions and location on the screen to be able to adjust the tool's location and dimension appropriate to the game.

--
--
Current features in development are:

[**DONE**] **Auto Macros**: This feature allows you to let a macro press a set of keys in a given interval, this will also allow people without a macro-keyboard to use keyboard-like macros. **(*1)**

[**5%**] **ToDo-List**:       The ToDo-List as it's name may reveal is to manage a list of tasks that you're about to do ingame. There will also be a feature to set reminding notifications for a given date.

[**0%**] **Energy notifier**: This allows you to get notified when one or more of your alternative characters has enough energy.

[**0%**] **Horse breeding calculator**: As the name despicts this will be a quick way to calculate the breeding chances of your horses within the game.

--
Planned / uncertain features:

[**0%**] **Party/Event platform**: This feature will give you the ability to form parties for a given purpose and a certain time since you cannot do this in-game right now. This will also be usable to recruit new guild members.

[**0%**] **Boss timers**: As you cannot reliably know when field- and worldbosses spawn in the game this feature will allow you to vote when a boss spawns and if a given threshold is passed this will be available for everyone as a potential boss spawn.

[**0%**] **Auto hotkey**: This feature is very uncertain since it would allow you to bind a key-combo to a certain hot-key so you can do combos with only button press.

--
--
Notes:

**(*1)** Since the client blocks messages directly send to the window's handle the macros are realized by a keyboard hook which means the window has to be in the foreground to receive the keys otherwise random applications would receive the keypresses which is not wanted in this case. That's why before every press the game's window will be set to the foreground which can result in some inconveniences when you're doing something else on your pc or even using this tool since it will lose the focus as well for a short amount of time.
