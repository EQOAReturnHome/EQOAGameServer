function  event_say(choice)
diagOptions = {}
    npcDialogue = "Don't go downstairs! Unless you enjoy being ripped apart by giants rats... err... in which case I charge 2 tunar."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end