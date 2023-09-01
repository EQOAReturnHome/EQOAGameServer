function event_say()
diagOptions = {}
    npcDialogue = "To the south of Winter's Deep Lake is a river, that flows from the mountain of Thedruk. Within this mountain is also the tunnel of Thedruk. It was once part a great dwarven mining operation, but it is now quite overrun with orcs. If you manage to make it through the tunnel, you can then travel to the dwarven city of Moradhim to the west. They are an eccentric people, but they are also our allies in the fight against evil."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end