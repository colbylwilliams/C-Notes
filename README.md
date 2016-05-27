## C Notes

C Notes (short for Contextual Notes) is an iOS app written in Xamarin, and the product of my experimenting with a library I’m calling `ContextKit`.

`ContextKit` silently gathers and saves “contextual” information and associates it with an _object_ every time the _object_ is relevant to the User.  In the case of C Notes, that _object_ is a simple note.

Each time the User “touches” (creates, views, updates, etc.) an _object_, `ContextKit` takes a “snapshot” of the current _context_, including location, time of day, upcoming and past calendar events, attendees of those events, etc.

Finally, any time the app is in use, `ContextKit` compares the snapshots with the User’s _current_ context to intuitively present content most relevant to that specific time and place.



### Technology

* [Xamarin][0]
* [Realm.io][1]
* [HockeyApp][2]



### Todo

* Add the `ContextKit` repo as a submodule
* Secure notes and Touch ID
* Continue to evolve the sorting algorithms



#### Contributors

* [colbylwilliams][3]



#### License

The MIT License (MIT)
Copyright (c) 2016 Colby Williams


[0]: https://www.xamarin.com/
[1]:https://realm.io/docs/xamarin/latest/
[2]:https://rink.hockeyapp.net
[3]:https://github.com/colbylwilliams