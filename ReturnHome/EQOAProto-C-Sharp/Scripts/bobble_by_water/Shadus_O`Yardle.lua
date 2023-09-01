function  event_say(choice)
diagOptions = {}
    npcDialogue = "Luck is something to make yer adventures even more profitable. If yer Interested, I could tell you more..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end