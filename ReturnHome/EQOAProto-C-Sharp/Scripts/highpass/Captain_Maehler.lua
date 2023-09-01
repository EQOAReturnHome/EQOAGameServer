function  event_say(choice)
diagOptions = {}
    npcDialogue = "Fear not, we will make keep this city safe if it's the last thing we do."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end