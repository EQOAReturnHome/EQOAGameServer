function  event_say(choice)
diagOptions = {}
    npcDialogue = "That giant gnome gives me the creeps.  I hope he doesn't go berserk and slaughter us all."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end