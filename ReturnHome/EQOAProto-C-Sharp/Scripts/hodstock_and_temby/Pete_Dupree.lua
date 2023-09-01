function  event_say(choice)
diagOptions = {}
    npcDialogue = "That's just what I thought, those damned bloodfin sharks are back. This is gonna mess up our whole operation."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end