class User < ApplicationRecord

  devise :database_authenticatable, :recoverable, :rememberable, :trackable, :validatable, :timeoutable, :lockable, :confirmable#, :omniauthable

end
