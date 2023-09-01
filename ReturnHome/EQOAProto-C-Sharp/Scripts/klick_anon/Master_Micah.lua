function  event_say(choice)
diagOptions = {}
    npcDialogue = "Name's Micah. I, steward of the temple, am a very busy gnome. I have no time for idle chit chat."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end