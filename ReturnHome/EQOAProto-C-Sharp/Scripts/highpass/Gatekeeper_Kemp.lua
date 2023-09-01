function  event_say(choice)
diagOptions = {}
    npcDialogue = "Not a single enemy shall pass through these gates.  Rest assured, citizen, I will give my life to keep you safe."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end