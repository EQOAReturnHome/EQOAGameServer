function  event_say(choice)
diagOptions = {}
    npcDialogue = "Oi!  'ave I got som'hin fer you!! It's fresh, sweet, and delicious!"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end