function  event_say(choice)
diagOptions = {}
    npcDialogue = "I'm looking out for mindwhippers right now. I wish to know more about their psychic powers."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end