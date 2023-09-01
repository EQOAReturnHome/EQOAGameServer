function  event_say(choice)
diagOptions = {}
    npcDialogue = "The wife's brickleberry pies are delicious! I volunteered for this patrol route to keep my steps up and the pie weight off."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end