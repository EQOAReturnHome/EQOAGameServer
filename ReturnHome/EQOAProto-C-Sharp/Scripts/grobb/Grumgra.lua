function  event_say(choice)
diagOptions = {}
    npcDialogue = "Grumgra wait for ship. Special delivery supposed to be here by now. Frogloks hopped through here just an hour ago. Grumgra hate waiting."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end