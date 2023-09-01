function  event_say(choice)
diagOptions = {}
    npcDialogue = "*zzzzzzzZZZZZZZ...BEEP*, \"TARGET ACQUIRED. STANDING BY.\""
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end