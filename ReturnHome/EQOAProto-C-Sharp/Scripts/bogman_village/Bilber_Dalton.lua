function  event_say(choice)
diagOptions = {}
    npcDialogue = "I used to do some decent fishing here, but not lately. Who knows what gets into the fish with all the nasty undead roaming about."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end