function  event_say(choice)
diagOptions = {}
    npcDialogue = "All trolls need fish. If you have fish, you eat anywhere. I teach you fishing now, here on Snotspit River. Ready?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end