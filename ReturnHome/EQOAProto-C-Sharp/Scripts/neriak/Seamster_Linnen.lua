function  event_say(choice)
diagOptions = {}
    npcDialogue = "Don't mind me, I'm just finishing my latest tailoring product. Would you like to know more about tailoring?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end