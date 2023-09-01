function  event_say(choice)
diagOptions = {}
    npcDialogue = "Looking for work? I'm sure there are folks around the glade that could use a hand will all sorts of tasks."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end