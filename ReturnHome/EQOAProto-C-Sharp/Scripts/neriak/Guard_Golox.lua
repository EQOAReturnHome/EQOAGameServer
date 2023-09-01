function  event_say(choice)
diagOptions = {}
    npcDialogue = "Should any enemies invade Neriak they shall be greeted by my blade."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end