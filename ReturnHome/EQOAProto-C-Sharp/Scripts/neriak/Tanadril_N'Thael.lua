function  event_say(choice)
diagOptions = {}
    npcDialogue = "You shame yourself with this intrusion. Leave me be."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end