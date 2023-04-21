# NOYITO2ChannelMicroUSBRelayDemo
Some demo code on how to interact with a NOYITO 2-Channel Micro USB Relay Module

This is a windows console app written in .net 4.6.1 that demonstrates how to turn on/off each relay side of a NOYITO 2-Channel Micro USB Relay Module.
The guts are in the USBPowerSwitch.cs class.  You pretty much instaiate the class which will auto detect what com port your power switch is hooked up to,
and then there are four commands you can issue to it.

Demo on how it works in Program.cs. 

Tweaking this code (adding more commands) will probably enable it to work with their 4 and 8 channel models as well.

Some interesting info here in the comments that helped me code for a project.  This demo is me giving back.
https://www.amazon.com/NOYITO-2-Channel-Module-Control-Intelligent/dp/B081RM7PMY/ref=sr_1_3?crid=T0POA7LV6OVN&keywords=usb+relay&qid=1680817548&sprefix=USB+Relay%2Caps%2C141&sr=8-3

-infocyde 23/04/21

Happy Coding!
