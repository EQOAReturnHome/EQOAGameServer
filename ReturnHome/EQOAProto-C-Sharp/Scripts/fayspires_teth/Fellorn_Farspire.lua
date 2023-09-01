function event_say()
diagOptions = {}
    npcDialogue = "Beyond the shores of Tunaria, far to the east, over the Ocean of Tears, is a land once called Faydewer. Eventually, we elves must leave this sanctuary and rejoin our long lost elven kind that left long ago. A secret I will share with you, playerName...we once knew a way to travel there by way of a wizards spire! But this knowledge has been lost in our tragic escape from Takish'Hiz. Perhaps we will find it again when the new spire has been built."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end