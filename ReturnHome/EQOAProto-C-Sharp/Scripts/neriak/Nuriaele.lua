function  event_say(choice)
diagOptions = {}
    npcDialogue = "The secret behind making the perfect jewelry is the passion within the maker. Are you interest in learning Jewelcrafting?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end